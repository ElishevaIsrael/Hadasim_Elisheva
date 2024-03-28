import { Component, OnInit } from '@angular/core';
import { MemberService } from '../services/mem.service';
import { Members, Vaccinations, CovidStatus } from '../mem'; // Import data models
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-members-list',
  templateUrl: './members-list.component.html',
  styleUrl: './members-list.component.css',
  providers: [DatePipe]
})
export class MembersListComponent implements OnInit {
  members: Members[] = [];
  newMember: Members = {} as Members;
  selectedMemberId: string | null = null; // Initialize with null
  vaccinationDetails: Vaccinations[] = []; // Initialize with null
  covidStatus: CovidStatus | null = null; // Initialize with null

  constructor(private memberService: MemberService, private http: HttpClient,public datePipe: DatePipe) { }

  ngOnInit(): void {
    this.memberService.getMembers().subscribe(data => this.members = data);
  }

  async fetchMemberDetails(memberId: string) {
    if (!memberId) {
      console.error("Member ID is undefined");
      return;
    }
    this.selectedMemberId = memberId;
    await this.waitForSelectedMemberId();
  
    // Fetch vaccination details
    this.http.get<Vaccinations[]>(`http://localhost:5000/api/vaccinationandcovid/vaccination/${memberId}`)
      .subscribe(vaccinationDetails => this.vaccinationDetails = vaccinationDetails);

    // Fetch COVID status
    this.http.get<CovidStatus>(`http://localhost:5000/api/vaccinationandcovid/covidstatus/${memberId}`)
      .subscribe(covidStatus => this.covidStatus = covidStatus);

  }
 async deleteMember(memberId: string) {
  console.log(memberId);
    if (!memberId) {
      console.error("Member ID is undefined");
      return;
    }
    this.memberService.deleteMember(memberId)
      .subscribe(response => {
        if (response === 204) { // No Content (member deleted)
          this.members = this.members.filter(member => member.idNumber !== memberId);
          console.log("Member deleted successfully!");
        } else {
          console.error("Error deleting member:", response);
        }
      });
  }

  
updateMember(member: Members) {
  this.memberService.updateMember(member)
    .subscribe(updatedMember => {
      // Update the member data in your local array (optional)
      const memberIndex = this.members.findIndex(m => m.idNumber === updatedMember.idNumber);
      if (memberIndex !== -1) {
        this.members[memberIndex] = updatedMember;
      }
      console.log("Member updated successfully!");
    }, error => {
      console.error("Error updating member:", error);
    });
}
addMember() {
  // Validate member data before sending the request (optional)
  if (!this.newMember.firstName || !this.newMember.lastName || !this.newMember.city || !this.newMember.street|| !this.newMember.number|| !this.newMember.dateOfBirth|| !this.newMember.phone|| !this.newMember.mobilePhone) {
    console.error("Please fill in all required member details.");
    return;
  }

  this.memberService.addMember(this.newMember)
    .subscribe(addedMember => {
      this.members.push(addedMember); // Add the new member to the local array (optional)
      this.newMember = {} as Members; // Reset the new member object for the next entry
      console.log("Member added successfully!");
    }, error => {
      console.error("Error adding member:", error);
    });
}


  private waitForSelectedMemberId(): Promise<void> {
    return new Promise<void>((resolve) => {
      const interval = setInterval(() => {
        if (this.selectedMemberId) {
          clearInterval(interval);
          resolve();
        }
      }, 100); // Check every 100 milliseconds
    });
  }
}




 


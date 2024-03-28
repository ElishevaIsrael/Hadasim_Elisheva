import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators'; 
import { Members } from '../mem';

@Injectable({ providedIn: 'root' })
export class MemberService {
  private apiUrl = 'http://localhost:5000/api/member'; 

  constructor(private http: HttpClient) {}

  getMembers(): Observable<Members[]> {
    return this.http.get<Members[]>(this.apiUrl);
  }
  deleteMember(memberId: string) {
    return this.http.delete('http://localhost:5000/api/member/MemberFromDb/' + memberId)
    .pipe(
      tap(() => {
        window.location.reload(); // Reload the page after deletion
      })
    ); 
 
  }
  updateMember(member: Members) {
    return this.http.put<Members>('http://localhost:5000/api/member/' + member.idNumber, member); 
  }
  addMember(member: Members) {
    return this.http.post<Members>('http://localhost:5000/api/member/AddPerson', member); // Add POST request for adding member
  }
}
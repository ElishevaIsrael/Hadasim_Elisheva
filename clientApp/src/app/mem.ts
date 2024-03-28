export interface Members {
  idNumber: string;
  firstName: string;
  lastName: string;
  city: string;
  street: string;
  number: string;
  dateOfBirth: Date;
  phone: string;
  mobilePhone: string;
}
  
  export interface Vaccinations {
    IdNumber: string;
    coronaVaccine: number;
    vaccineDate: Date;
    vaccineManufacturer:string;
  
  }
  export interface CovidStatus{
    IdNumber:string;
    positiveTestDate:Date;
    recoveryDate:Date;
  }
  
  
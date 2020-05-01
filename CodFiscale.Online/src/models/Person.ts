import {Gender} from "@/models/Gender";
import {Place} from "@/models/Place";

export class Person {
    private name: string | null;
    private surname: string | null;
    private gender: Gender;
    private birthDate: Date | null;
    private birthPlaceId: number | null;
    get BirthPlaceId() {
        return this.birthPlaceId;
    }

    set BirthPlaceId(value) {
        this.birthPlaceId = value;
    }
    get BirthDate(){
        return this.birthDate;
    }

    set BirthDate(value) {
        this.birthDate = value;
    }
    get Gender() {
        return this.gender;
    }
    set Gender(value: Gender | string) {
        // Smelly
       if (typeof(value) == "string") {
           const initial = value.substr(0, 1).toUpperCase();
           if (initial === 'F') {
               this.gender = Gender.Female;
           }
           else if (initial === 'M') {
               this.gender = Gender.Male;
           }
           else {
               this.gender = Gender.Unspecified;
           }
       }
       else {
           this.gender = value;
       }

    }
    get Surname() {
        return this.surname;
    }

    set Surname(value) {
        this.surname = value;
    }

    get Name() {
        return this.name;
    }

    set Name(value) {
        this.name = value;
    }

    constructor() {
        this.name = "";
        this.surname = "";
        this.gender = Gender.Unspecified;
        this.birthDate = null;
        this.birthPlaceId = 0;
    }
}

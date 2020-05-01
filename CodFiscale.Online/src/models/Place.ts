export class Place {
    private name: string;
    private province: string;
    private provinceAbbreviation: string;
    private region: string;
    private code: string;
    private id?: number;

    get Id() {
        const id = this.id;
        if (id == undefined) {
            return 0;
        }
        return id;
    }

    set Id(value: number) {
        this.id = value;
    }

    set Code(value) {
        if (value != null && value.length != 4) {
            throw new Error("The code must be 4 characters long.");
        }
        this.code = value;
    }

    get Code() {
        return this.code;
    }

    get Region() {
        return this.region;
    }

    set Region(value) {
        this.region = value;
    }

    get ProvinceAbbreviation() {
        return this.provinceAbbreviation;
    }

    set ProvinceAbbreviation(value) {
        if (value != null && value.length !== 2) {
            throw new Error("Province abbreviation can either be null or a two-character-long string.");
        }
        this.provinceAbbreviation = value;
    }
    get Province() {
        return this.province;
    }

    set Province(value) {
        this.province = value;
    }
    get Name() {
        return this.name;
    }
    set Name(value) {
        this.name = value;
    }
    constructor() {
        this.name = "";
        this.province = "";
        this.provinceAbbreviation = "";
        this.region = "";
        this.code = "";
    }

    public getPrettyName() {
        return this.name + " (" + this.provinceAbbreviation + ")";
    }


}

export interface Claim {
    type: string,
    value: string
}

export class User {
    private readonly claims: Claim[]

    constructor(claims: Claim[]) {
        this.claims = claims
    }

    public get id() {
        return this.claims.find(c => c.type == 'id')?.value
    }

    public get name() {
        return this.claims.find(c => c.type == 'name')?.value
    }

    public get email() {
        return this.claims.find(c => c.type == 'email')?.value
    }

    public get college() {
        return this.claims.find(c => c.type == 'college')?.value
    }
}

export interface Class {
    name: string
    absences: number
    total: number
    isActive: boolean
    times: Time[]
    userId?: string
    id?: string
}

export interface Time {
    dayOfWeek: number
    hour: number
    minute: number
    classId?: string
    id?: string
}

export interface DayOfWeek {
    day: number;
    str: string;
}
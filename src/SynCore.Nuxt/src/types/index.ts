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
    times: ClassTime[]
    userId?: string
    id?: string,
    edit: boolean
}

export interface ClassTime {
    dayOfWeek: number
    startHour: string
    startMinute: string
    endHour: string
    endMinute: string
    classId?: string
    id?: string
}

export interface DayOfWeekSchedule {
    dayOfWeek: number
    classes: ClassSchedule[]
}

export interface ClassSchedule {
    name: string
    absences: number
    total: number
    freq: string
    dayOfWeek: number
    startHour: string
    startMinute: string
    startTime: string
    endHour: string
    endMinute: string
    endTime: string
}

export interface SecondaryUser {
    id: string,
    name: string
}

export interface ChatMessage {
    id: string,
    text: string,
    owner: SecondaryUser,
    to: SecondaryUser
}
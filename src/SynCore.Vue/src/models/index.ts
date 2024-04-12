export interface ErrorResponse {
    errorMessage: string,
}

export interface Claim {
    type: string,
    value: string
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

export class User {
    id: string;
    name: string;
    college: string;
    email: string;

    constructor(claims: Claim[]) {
        this.id = ''
        this.name = ''
        this.college = ''
        this.email = ''

        claims.forEach(c => {
            if (c.type === 'id') {
                this.id = c.value
            } else if (c.type === 'name'){
                this.name = c.value
            } else if (c.type === 'email') {
                this.email = c.value
            } else if (c.type === 'college') {
                this.college = c.value
            }
        })
    }
}




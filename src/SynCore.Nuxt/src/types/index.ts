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
        return this.claims.find(c => c.type == 'id')
    }

    public get name() {
        return this.claims.find(c => c.type == 'name')
    }

    public get email() {
        return this.claims.find(c => c.type == 'email')
    }

    public get college() {
        return this.claims.find(c => c.type == 'college')
    }
}
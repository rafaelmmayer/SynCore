interface Claim {
    type: string,
    value: string
}

interface ErrorResponse {
    errorMessage: string,
}

class User {
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

export { User }
export type { Claim, ErrorResponse }
export const daysOfWeek = [
    {
        value: 0,
        text: 'Domingo'
    },
    {
        value: 1,
        text: 'Segunda'
    },
    {
        value: 2,
        text: 'Terça'
    },
    {
        value: 3,
        text: 'Quarta'
    },
    {
        value: 4,
        text: 'Quinta'
    },
    {
        value: 5,
        text: 'Sexta'
    },
    {
        value: 6,
        text: 'Sábado'
    }
]

export function hours() {
    const hours = [];
    for (let hour = 0; hour < 24; hour++) {
        hours.push(hour.toString().padStart(2, '0'));
    }
    return hours;
}

export function minutes() {
    const minutes = [];
    for (let minute = 0; minute < 60; minute += 5) {
        minutes.push(minute.toString().padStart(2, '0'));
    }
    return minutes;
}
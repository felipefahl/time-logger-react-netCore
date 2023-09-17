export interface TimeLog {
    id: string;
    projectId: string;
    durationMinutes: string;
    note: string;
    createdAt: Date;
}

export interface TimeLogArray extends Array<TimeLog> {}
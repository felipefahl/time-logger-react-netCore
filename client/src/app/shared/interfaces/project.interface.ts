export interface Project {
    id: string;
    name: string;
    deadLine: Date;
    closedAt?: Date | undefined;
}

export interface ProjectArray extends Array<Project> {}
const BASE_URL = "http://localhost:3001/api/v1";

export async function getAll(sortByDeadline: boolean, onlyActives: boolean)  {
    const response = await fetch(`${BASE_URL}/projects/?sortByDeadline=${sortByDeadline}&onlyActives=${onlyActives}`);

    await (LogResponse(response));
    return response.json();
}

export async function getAllProjectTimeLog(projectId: string | undefined)  {
    if(!projectId)
        return;
    const response = await fetch(`${BASE_URL}/projects/${projectId}/timeLogs`);

    await (LogResponse(response));
    return response.json();
}

export async function postProjectTimeLog(projectId: string | undefined, durationMinutes: number, note:string, projectFinished: boolean)  {

    if(!projectId)
        return;

    var data = {
        durationMinutes : Number(durationMinutes),
        note,
        projectFinished
    };

    console.log(JSON.stringify(data));

    const response = await fetch(`${BASE_URL}/projects/${projectId}/timeLogs`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    });

    await (LogResponse(response));
}

export async function postProject(data: any)  {

    if(!data)
        return;

    console.log(JSON.stringify(data));

    const response = await fetch(`${BASE_URL}/projects`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    });

    await (LogResponse(response));
}

async function LogResponse(response:Response){
    if (response.status === 200) {
        console.log('Time logged successfully.');
    } else if (response.status === 400) {
        const errorData = await response.json();
        console.error('Validation errors:', errorData);
    } else {
        console.error('An error occurred while logging time.');
    }
}

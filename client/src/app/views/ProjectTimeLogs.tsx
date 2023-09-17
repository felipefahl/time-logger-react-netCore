import React, { useContext, useEffect, useState } from "react";
import { getAllProjectTimeLog } from "../api/projects";
import { SelectedProjectContext, SelectedProjectType } from "../hooks/SelectedProjectProvider";
import { TimeLogArray } from "../shared/interfaces/projectTimeLog.interface";
import TableTimeLog from "../components/TableTimelog";
import Header from "../components/Header";
import { Link } from "react-router-dom";

export default function Projects() {    
    const { selectedPoject } = useContext(SelectedProjectContext) as SelectedProjectType;

    const [timeLogs, setTimeLogs] = useState<TimeLogArray>([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await getAllProjectTimeLog(selectedPoject?.id);
                setTimeLogs(data);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };

        fetchData();
    }, [selectedPoject]);

    return (
        <>
           <Header />
           <main>            
           <div className="container mx-auto">
                {timeLogs.length > 0 && 
                    <>
                        
                        <h2><span style={{fontWeight: 'bold'}}>Project ID: </span>{selectedPoject?.id}</h2>
                        <h2><span style={{fontWeight: 'bold'}}>Project Name: </span>{selectedPoject?.name}</h2>
                        <TableTimeLog timeLogs={timeLogs} />   
                    </>
                }
                {timeLogs.length == 0 && <h1>"There is no timelog registered for this project!"</h1>}           

                <Link className="inline-block p-5 rounded-md bg-blue-100 font-semibold text-black gap-1 hover:bg-blue-200" to="/" relative="path">
                    Back
                </Link> 
            </div>
        </main>
        </>
    );
}

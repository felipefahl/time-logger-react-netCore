import React, { useContext } from "react";
import { Project, ProjectArray } from "../shared/interfaces/project.interface";
import { useNavigate } from "react-router-dom";
import { SelectedProjectContext, SelectedProjectType } from "../hooks/SelectedProjectProvider";

interface Props {
    projects: ProjectArray;
  }

export default function Table({projects}: Props) {    

    const navigate = useNavigate();
    const { selectPoject } = useContext(SelectedProjectContext) as SelectedProjectType;

    const handleAddTimeLog = (project : Project) => {
        selectPoject(project);
        navigate('/add-timelog');
      };

      const handleSeeTimeLogs = (project : Project) => {
        selectPoject(project);
        navigate('/timelogs');
      };

    return (
        <div>           
            <table className="table-fixed w-full">
                <thead className="bg-gray-200">
                    <tr>
                        <th className="border px-4 py-2 w-12">#</th>
                        <th className="border px-4 py-2">Project Name</th>
                        <th className="border px-4 py-2">Deadline</th>
                        <th className="border px-4 py-2">Closed at</th>
                        <th className="border px-4 py-2"/>
                        <th className="border px-4 py-2"/>
                    </tr>
                </thead>
                <tbody>
                    {projects.map((project : Project, index) => (
                        <tr key={project.id}>
                            <td className="border px-4 py-2 w-12">{index + 1}</td>
                            <td className="border px-4 py-2">{project.name}</td>
                            <td className="border px-4 py-2">{new Date(project.deadLine).toUTCString()}</td>
                            <td className="border px-4 py-2">{project.closedAt && new Date(project.closedAt).toUTCString()}</td>
                            <td className="border px-4 py-2"> 
                                {!project.closedAt &&  <button
                                    className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-1 px-2 rounded"
                                    onClick={() => handleAddTimeLog(project)}
                                    >
                                    Add TimeLog
                                </button>
                            }   
                            </td>
                            <td className="border px-4 py-2"> 
                                 <button
                                    className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-1 px-2 rounded"
                                    onClick={() => handleSeeTimeLogs(project)}
                                    >
                                    See TimeLogs
                                </button>
                            </td>
                           
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}


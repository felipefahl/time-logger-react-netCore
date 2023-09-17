import React from "react";
import { TimeLog, TimeLogArray } from "../shared/interfaces/projectTimeLog.interface";

interface Props {
    timeLogs: TimeLogArray;
  }

export default function TableTimeLog({timeLogs}: Props) {   
    return (
        <div>           
            <table className="table-fixed w-full">
                <thead className="bg-gray-200">
                    <tr>
                        <th className="border px-4 py-2 w-12">#</th>
                        <th className="border px-4 py-2">Duration (Minutes)</th>
                        <th className="border px-4 py-2">Note</th>
                        <th className="border px-4 py-2">Created At</th>
                    </tr>
                </thead>
                <tbody>
                    {timeLogs.map((timeLog : TimeLog, index) => (
                        <tr key={timeLog.id}>
                            <td className="border px-4 py-2 w-12">{index + 1}</td>
                            <td className="border px-4 py-2">{timeLog.durationMinutes}</td>
                            <td className="border px-4 py-2">{timeLog.note}</td>
                            <td className="border px-4 py-2">{new Date(timeLog.createdAt).toUTCString()}</td>                           
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}


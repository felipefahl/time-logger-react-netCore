import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import TimeLogForm from "./TimeLogForm";
import Header from "../components/Header";
import { useSelectedProject } from "../hooks/SelectedProjectProvider";

export default function AddTimeLogger() {
    const { selectedPoject } = useSelectedProject();
    const navigate = useNavigate();
    

    useEffect(() => {
        if(!selectedPoject)
            navigate('/');
    }, [selectedPoject])

    return (
        <>
            <Header />

            <main>
                <div className="container mx-auto">
                    <TimeLogForm />
                </div>
            </main>
        </>
    );
}

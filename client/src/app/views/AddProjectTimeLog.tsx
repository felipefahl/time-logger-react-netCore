import React, { useContext, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { SelectedProjectContext, SelectedProjectType } from "../hooks/SelectedProjectProvider";
import TimeLogForm from "./TimeLogForm";
import Header from "../components/Header";

export default function AddTimeLogger() {
    const { selectedPoject } = useContext(SelectedProjectContext) as SelectedProjectType;
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

import React from "react";
import ProjectForm from "./ProjectForm";
import Header from "../components/Header";

export default function AddProject() {
    return (
        <>
            <Header />

            <main>
                <div className="container mx-auto">
                    <ProjectForm />
                </div>
            </main>
        </>
    );
}

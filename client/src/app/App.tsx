import * as React from "react";
import Projects from "./views/Projects";
import "./style.css";
import Header from "./components/Header";

export default function App() {
    return (
        <>
           <Header />

            <main>
                <div className="container mx-auto">
                    <Projects />
                </div>
            </main>
        </>
    );
}

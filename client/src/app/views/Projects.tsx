import React, { ChangeEvent, FormEvent, useEffect, useState } from "react";
import Table from "../components/Table";
import { getAll } from "../api/projects";
import FilterCheckbox from "../components/FilterCheckbox";
import { ProjectArray } from "../shared/interfaces/project.interface";
import { Link } from "react-router-dom";

export default function Projects() {
    
    const [searchField, setSearchField] = useState("");
    const [filterProject, setFilterProject] = useState("");
    const [projects, setProjects] = useState<ProjectArray>([]);
    const [filteredProjects, setFilteredProjects] = useState<ProjectArray>([]);
    const [sortByDeadline, setSortByDeadline] = useState(false);
    const [onlyActives, setOnlyActives] = useState(false);

    useEffect(() => {
        setSearchField("");
        const fetchData = async () => {
            try {
                const data = await getAll(sortByDeadline, onlyActives);
                setProjects(data);
                setFilteredProjects(data);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };

        fetchData();
    }, [sortByDeadline, onlyActives]);

    useEffect(() => {
        if(filterProject){
            setFilteredProjects(projects.filter(item => item.name.toLowerCase().includes(filterProject)))
        }
    }, [filterProject]);

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();    
        if(searchField)
            setFilterProject(searchField.toLowerCase());
      };

      const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const { value } = e.target;
        setSearchField(value);

        if(!value)
            setFilteredProjects(projects);
    };

    return (
        <>
            <div className="flex items-center my-6">
                <div className="w-1/2">
                    <Link className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" to="/add-project" relative="path">
                        Add Project
                    </Link>
                </div>

                <div className="w-1/2 flex justify-end">
                    <form onSubmit={handleSubmit}>
                        <input
                            className="border rounded-full py-2 px-4"
                            type="search"
                            placeholder="Search"
                            aria-label="Search"
                            onChange={handleChange}
                            value={searchField}
                        />
                        <button
                            className="bg-blue-500 hover:bg-blue-700 text-white rounded-full py-2 px-4 ml-2"
                            type="submit"
                        >
                            Search
                        </button>
                    </form>
                </div>
            </div>
            <div>
                <FilterCheckbox
                    labelText="Sort by Deadline:"
                    checked={sortByDeadline}
                    onChange={() => setSortByDeadline(!sortByDeadline)}
                />
            </div>
            <div>
                <FilterCheckbox
                    labelText="Only Actives:"
                    checked={onlyActives}
                    onChange={() => setOnlyActives(!onlyActives)}
                />         
            </div>
            <Table projects={filteredProjects} />
        </>
    );
}

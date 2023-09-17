import React, { ReactNode, useState } from 'react';
import { Project } from '../shared/interfaces/project.interface';

interface Props {
    children?: ReactNode
}

export type SelectedProjectType = {
    selectedPoject: Project | undefined;
    selectPoject: (project: Project) => void;
  };

export const SelectedProjectContext = React.createContext<SelectedProjectType | null>(null);

export default function SelectedProjectProvider({children} : Props) {
    const [selectedPoject, setSelectedPoject] = useState<Project>();

    const selectPoject = (project: Project) => {
        setSelectedPoject(project)
      }

    return (
        <SelectedProjectContext.Provider value={{selectedPoject, selectPoject}}>
            {children}
        </SelectedProjectContext.Provider>
    );
}
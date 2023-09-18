import React, { useContext, useState } from 'react';
import { Project } from '../shared/interfaces/project.interface';

interface SelectedProjectType {
    selectedPoject: Project | undefined;
    selectPoject: (project: Project) => void;
  };

const SelectedProjectContext = React.createContext<SelectedProjectType>({} as SelectedProjectType);

const SelectedProjectProvider: React.FC = ({ children }) => {
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

function useSelectedProject(): SelectedProjectType {
    const context = useContext(SelectedProjectContext);
    return context;
  }
  
  export { SelectedProjectProvider, useSelectedProject };
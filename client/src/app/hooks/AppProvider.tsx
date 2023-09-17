import React, { ReactNode } from 'react';
import SelectedProjectProvider from './SelectedProjectProvider';

interface Props {
    children?: ReactNode
}

export default function AppProvider({children} : Props) {

    return (
        <SelectedProjectProvider>
            {children}
        </SelectedProjectProvider>
    );
}
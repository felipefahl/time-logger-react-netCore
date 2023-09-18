import React from "react";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import App from "../App";
import ErrorPage from "../views/ErrorPage";
import AddProjectTimeLog from "../views/AddProjectTimeLog";
import ProjectTimeLogs from "../views/ProjectTimeLogs";

export default function Routes() {    
    const router = createBrowserRouter([
        {
          path: "/",
          element: <App />,
          errorElement: <ErrorPage />,
        },
        {
            path: "/add-timelog",
            element: <AddProjectTimeLog />,
            errorElement: <ErrorPage />,
        },
        {
            path: "/timelogs",
            element: <ProjectTimeLogs />,
            errorElement: <ErrorPage />,
        },
        {
            path: "/add-project",
            element: <AddTimeLogger />,
            errorElement: <ErrorPage />,
        },
      ]);
      
        return (
            <React.StrictMode>
                <RouterProvider router={router} />
            </React.StrictMode>
        );
    }
import * as React from "react";
import * as ReactDOM from "react-dom";
import Routes from "./app/routes/Routes";
import AppProvider from "./app/hooks/AppProvider";

ReactDOM.render(
    <AppProvider>
        <Routes/>
    </AppProvider>
, 
document.getElementById("root"));

import * as React from "react";
import { useRouteError } from "react-router-dom";
import Header from "../components/Header";

interface Error {
    statusText: string;
    message: string;
}

export default function ErrorPage() {
  const error = useRouteError() as Error;
  console.error(error);

  return (
    <div id="error-page">
      <Header />
      <h1>Oops!</h1>
      <p>Sorry, an unexpected error has occurred.</p>
      <p>
        <i>{error.statusText || error.message}</i>
      </p>
    </div>
  );
}
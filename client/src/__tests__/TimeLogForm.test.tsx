import { fireEvent, render, waitFor } from "@testing-library/react";
import React from "react";
import TimeLogForm from "../app/views/TimeLogForm";

const mockedHistoryPush = jest.fn();
const mockedSignIn = jest.fn();
const mockedAddToast = jest.fn();

jest.mock('../api/projects', () => {
  return {
    postProjectTimeLog: () => ({ push: mockedHistoryPush }),
    Link: ({ children }: { children: React.ReactNode }) => children,
  };
});

jest.mock('react-router-dom', () => {
  return {
    useHistory: () => ({ push: mockedHistoryPush }),
    Link: ({ children }: { children: React.ReactNode }) => children,
  };
});

jest.mock('../../hooks/auth', () => {
  return {
    useAuth: () => ({
      signIn: mockedSignIn,
    }),
  };
});

jest.mock('../../hooks/toast', () => {
  return {
    useToast: () => ({
      addToast: mockedAddToast,
    }),
  };
});

describe("<TimeLogForm />", () => {
  test("should display a blank login form when render", async () => {
    const { getByPlaceholderText, getByText, findByTestId } = render(<TimeLogForm />);
    const timelogForm = await findByTestId("timelog-form");

    const emailField = getByPlaceholderText('E-mail');
    const emailPasswordField = getByPlaceholderText('Senha');
    const buttonElement = getByText('Entrar');

    fireEvent.change(emailField, {
      target: { value: 'email@email.com' },
    });
    fireEvent.change(emailPasswordField, {
      target: { value: '123321' },
    });

    fireEvent.click(buttonElement);

    await waitFor(() => {
      expect(timelogForm).toHaveFormValues({
        username: "",
      });
    });
  });
});


//Example
test("should submit the form with username, password, and remember", async () => {
  const onSubmit = jest.fn();
  const { findByTestId } = renderLoginForm({
    onSubmit,
    shouldRemember: false
  });
  const username = await findByTestId("username");
  const password = await findByTestId("password");
  const remember = await findByTestId("remember");
  const submit = await findByTestId("submit");

  fireEvent.change(username, { target: { value: "test" } });
  fireEvent.change(password, { target: { value: "password" } });
  fireEvent.click(remember);
  fireEvent.click(submit);

  expect(onSubmit).toHaveBeenCalledWith("test", "password", true);
});

import { fireEvent, render } from "@testing-library/react";
import React from "react";
import ProjectForm from "../app/views/ProjectForm";


const mockedNavigate = jest.fn();

jest.mock('../app/api/projects', () => ({
    postProject: jest.fn(),
}));

jest.mock('react-router-dom', () => {
  return {
    Link: ({ children }: { children: React.ReactNode }) => children,
    useNavigate: () => mockedNavigate
  };
});

describe('ProjectForm', () => {
  it('submits the form successfully when "Save" button is clicked', async () => {
    const { findByTestId, findByText } = render(<ProjectForm />);
    
    const nameMinutesInput = await findByTestId('name') as HTMLInputElement ;
    const deadlineInput = await findByTestId('deadline') as HTMLInputElement ;
    
    fireEvent.change(nameMinutesInput, { target: { value: 'name project Test' } });
    fireEvent.change(deadlineInput, { target: { value: `${(new Date()).toISOString().split('T')[0]}` } });
    
    fireEvent.click(await findByTestId('project-submit'));

    const successMessage = await findByText('Form has been submitted successfully');
    expect(successMessage).not.toBeNull;
  });

  it('displays an error message deadline less than today', async () => {
    const { findByTestId } = render(<ProjectForm />);
    
    const nameMinutesInput = await findByTestId('name') as HTMLInputElement ;
    const deadlineInput = await findByTestId('deadline') as HTMLInputElement ;
    
    fireEvent.change(nameMinutesInput, { target: { value: '30' } });
    fireEvent.change(deadlineInput, { target: { value: `${(new Date(Date.now() - 86400000)).toISOString().split('T')[0]}` } });

    fireEvent.click(await findByTestId('project-submit'));
    
    const errorMessage = await findByTestId('Must be greater than or equal to today');
    expect(errorMessage).not.toBeNull;
    
    expect(deadlineInput.value).toBe(`${(new Date(Date.now() - 86400000)).toISOString().split('T')[0]}`);
  });
});

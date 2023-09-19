import { fireEvent, render, waitFor } from "@testing-library/react";
import React from "react";
import TimeLogForm from "../app/views/TimeLogForm";


const mockedNavigate = jest.fn();

jest.mock('../app/api/projects', () => ({
  postProjectTimeLog: jest.fn(),
}));

jest.mock('../app/hooks/SelectedProjectProvider', () => {
  return {
    useSelectedProject: () => ({
      selectedPoject: { id: '5ba69926-af27-4a2c-b89d-a96d0b0c765c', name: 'Test Project' },
    }),
  };
});

jest.mock('react-router-dom', () => {
  return {
    Link: ({ children }: { children: React.ReactNode }) => children,
    useNavigate: () => mockedNavigate
  };
});

describe('TimeLogForm', () => {
  it('submits the form successfully when "Log Time" button is clicked', async () => {
    const { findByTestId, findByText } = render(<TimeLogForm />);
    
    const durationMinutesInput = await findByTestId('durationMinutes') as HTMLInputElement ;
    const notesInput = await findByTestId('note') as HTMLInputElement ;
    
    fireEvent.change(durationMinutesInput, { target: { value: '30' } });
    fireEvent.change(notesInput, { target: { value: 'Test notes' } });
    
    fireEvent.click(await findByTestId('log-time-submit'));

    fireEvent.click(await findByText("Yes"));  
    
    
    jest.spyOn(window, 'alert').mockImplementation(() => {});

    await waitFor(() => expect(global.alert).toHaveBeenCalledWith('Project finished successfully'));
    
    expect(durationMinutesInput.value).toBe('');
    expect(notesInput.value).toBe('');
  });

  it('displays an error message for duration less than 30 minutes', async () => {
    const { findByTestId } = render(<TimeLogForm />);
    
    const durationMinutesInput = await findByTestId('durationMinutes') as HTMLInputElement ;
    const notesInput = await findByTestId('note') as HTMLInputElement ;
    
    fireEvent.change(durationMinutesInput, { target: { value: '29' } });
    fireEvent.change(notesInput, { target: { value: 'Test notes' } });

    fireEvent.click(await findByTestId('log-time-submit'));    
    
    jest.spyOn(window, 'alert');

    await waitFor(() => expect(global.alert).not.toHaveBeenCalledWith('Project finished successfully'));
    
    const errorMessage = await findByTestId('Must be greater than or equal to 30');
    expect(errorMessage).not.toBeNull;
    
    expect(durationMinutesInput.value).toBe("29");
  });
});

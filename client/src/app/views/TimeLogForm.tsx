import React, { useState, useContext } from 'react';
import { SelectedProjectContext, SelectedProjectType } from '../hooks/SelectedProjectProvider';
import { FormProvider, useForm } from 'react-hook-form';
import { durationMinutes_validation, notes_validation } from '../shared/utils/inputValidations';
import { BsFillCheckSquareFill } from 'react-icons/bs';
import { Link, useNavigate } from 'react-router-dom';
import { postProjectTimeLog } from '../api/projects';
import Input from '../components/TextInput';
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';

interface AddTimeLog{
    durationMinutes: number;
    note: string;
}

export default function TimeLogForm() {
    
    const { selectedPoject } = useContext(SelectedProjectContext) as SelectedProjectType;
    const [success, setSuccess] = useState(false);

    const navigate = useNavigate();
    const methods = useForm();

    const saveProjectTimeLog = async (data : any, projectFinished: boolean) => {
        setSuccess(false);

        try{            
            const addTimeLog = {
                ...data,
            } as AddTimeLog;

            await postProjectTimeLog(selectedPoject?.id, addTimeLog.durationMinutes, addTimeLog.note, projectFinished);
        } catch (error) {
             console.error('An error occurred:', error);
        }

        if(projectFinished){
            window.alert("Project finished com successfully");
            navigate('/');
        }

        methods.reset();
        setSuccess(true);
    };

    const onSubmit = methods.handleSubmit(async data => await 
        confirmAlert({
            title: 'Finishing the project',
            message: 'Do you want to finish the project?',
            buttons: [
              {
                label: 'Yes',
                onClick: async () => await saveProjectTimeLog(data, true)
              },
              {
                label: 'No',
                onClick: async () => await saveProjectTimeLog(data, false)
              }
            ]
          }));

    return (
        <div>            
            <h2>Log Time</h2>
            <FormProvider {...methods}>
                <form
                    onSubmit={e => e.preventDefault()}
                    noValidate
                    autoComplete="off"
                    className="container"
                >
                    <div className="grid gap-5 md:grid-cols-2">
                        <h2><span style={{fontWeight: 'bold'}}>Project ID: </span>{selectedPoject?.id}</h2>
                        <h2><span style={{fontWeight: 'bold'}}>Project Name: </span>{selectedPoject?.name}</h2>
                        <Input {...durationMinutes_validation}/>
                        <Input {...notes_validation} className="md:col-span-2" />
                    </div>
                    <div className="mt-5">
                        {success && (
                            <p className="font-semibold text-green-500 mb-5 flex items-center gap-1">
                            <BsFillCheckSquareFill /> Form has been submitted successfully
                            </p>
                        )}
                    <div>
                        
                    </div>
                    <button
                         onClick={onSubmit}       
                        className="inline-block p-5 rounded-md bg-blue-600 font-semibold text-white flex items-center gap-1 hover:bg-blue-800"
                    >
                        Log Time
                    </button>                      
                        <Link className="inline-block p-5 rounded-md bg-blue-100 font-semibold text-black gap-1 hover:bg-blue-200" to="/" relative="path">
                            Cancel
                        </Link>
                    </div>
                </form>
            </FormProvider>
        </div>
    );
}

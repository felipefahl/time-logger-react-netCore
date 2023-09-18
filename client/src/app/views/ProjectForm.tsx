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

export default function ProjectForm() {
    
    const [success, setSuccess] = useState(false);

    const navigate = useNavigate();
    const methods = useForm();

    const onSubmit = methods.handleSubmit(async data =>
       {
        setSuccess(false);

        try{ 
            await postProjectTimeLog(data);
        } catch (error) {
            console.error('An error occurred:', error);
        }

        methods.reset();
        setSuccess(true);
       });

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
                        <h1><span style={{fontWeight: 'bold'}}>New Project: </span></h1>
                        <Input {...projectName_validation}/>
                        <Input {...projectDeadline_validation}/>
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
                        Save
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

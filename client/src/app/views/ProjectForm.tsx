import React, { useState } from 'react';
import { FormProvider, useForm } from 'react-hook-form';
import { BsFillCheckSquareFill } from 'react-icons/bs';
import { Link } from 'react-router-dom';
import { postProject } from '../api/projects';
import Input from '../components/TextInput';
import 'react-confirm-alert/src/react-confirm-alert.css';
import { projectDeadline_validation, projectName_validation } from '../shared/utils/inputValidations';

export default function ProjectForm() {
    
    const [success, setSuccess] = useState(false);
    const methods = useForm();

    const onSubmit = methods.handleSubmit(async data =>
       {
        setSuccess(false);

        try{ 
            await postProject(data);
        } catch (error) {
            console.error('An error occurred:', error);
        }

        methods.reset();
        setSuccess(true);
       });

    return (
        <div>            
        <h1><span style={{fontWeight: 'bold'}}>New Project: </span></h1>
            <FormProvider {...methods}>
                <form
                    data-testid="project-form"
                    onSubmit={e => e.preventDefault()}
                    noValidate
                    autoComplete="off"
                    className="container"
                >
                    <div className="grid gap-5 md:grid-cols-2">
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
                        data-testid="project-submit"
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

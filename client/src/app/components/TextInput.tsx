import React, { Attributes, InputHTMLAttributes} from 'react';
import cn from 'classnames';
import { FieldPath, FieldValues, RegisterOptions, useFormContext } from 'react-hook-form';
import { findInputError } from '../shared/utils/findInputError';
import { MdError } from 'react-icons/md';
import { isFormInvalid } from '../shared/utils/isFormInvalid';
import { AnimatePresence, motion } from 'framer-motion';

interface TextInputProps extends InputHTMLAttributes<HTMLInputElement> {
    label: string;
    name: string;
    multiline?: boolean | undefined;
    validation?: RegisterOptions<FieldValues, FieldPath<FieldValues>> | undefined;
}

export default function Input ({
    name,
    label,
    type,
    id,
    placeholder,
    validation,
    multiline,
    className, 
    required, 
    disabled,
    min,
    max
  } : TextInputProps) {
    const {
      register,
      formState: { errors },
    } = useFormContext()
  
    const inputErrors = findInputError(errors, name)
    const isInvalid = isFormInvalid(inputErrors)
  
    const input_tailwind =
      'p-5 font-medium rounded-md w-full border border-slate-300 placeholder:opacity-60'
  
    return (
      <div className={cn('flex flex-col w-full gap-2', className)}>
        <div className="flex justify-between">
          <label htmlFor={id} className="font-semibold capitalize">
            {label}
          </label>
          <AnimatePresence initial={false}>
            {isInvalid && (
              <InputError
                message={inputErrors.error.message}
                key={inputErrors.error.message}
              />
            )}
          </AnimatePresence>
        </div>
        {multiline ? (
          <textarea
            id={id}
            data-testid={id}
            required={required} 
            disabled={disabled}
            className={cn(input_tailwind, 'min-h-[10rem] max-h-[20rem] resize-y')}
            placeholder={placeholder} 
            {...register(name, validation)}
          ></textarea>
        ) : (
          <input
            id={id}
            data-testid={id}
            required={required} 
            disabled={disabled}
            type={type}
            className={cn(input_tailwind)}
            placeholder={placeholder}
            min={min}
            max={max}
            {...register(name, validation)}
          />
        )}
      </div>
    )
  }
  
  interface InputError extends Attributes{
    message: string | undefined;
  }
  const InputError = function ({ message } : InputError) {
    return (
      <motion.p
        data-testid={message}
        className="flex items-center gap-1 px-2 font-semibold text-red-500 bg-red-100 rounded-md"
        {...framer_error}
      >
        <MdError />
        {message}
      </motion.p>
    )
  }
  
  const framer_error = {
    initial: { opacity: 0, y: 10 },
    animate: { opacity: 1, y: 0 },
    exit: { opacity: 0, y: 10 },
    transition: { duration: 0.2 },
  }


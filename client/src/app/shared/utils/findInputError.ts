import { FieldError, FieldErrors } from "react-hook-form"

export type InputErrorResponse = {
    error: FieldError;
};

export function findInputError(errors: FieldErrors<any>, name: string) : InputErrorResponse {
    const filtered = Object.keys(errors)
      .filter(key => key.includes(name))
      .reduce((cur, key) => {
        return Object.assign(cur, { error: errors[key] })
      }, {})
    return filtered as InputErrorResponse;
  }
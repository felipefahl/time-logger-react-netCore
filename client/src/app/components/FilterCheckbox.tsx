import React, { ChangeEventHandler } from 'react';

interface IFilterCheckboxProps {
    labelText: string;
    checked: boolean;
    onChange?: ChangeEventHandler<HTMLInputElement> | undefined;
}

export default function FilterCheckbox({ labelText, checked, onChange} : IFilterCheckboxProps) {
  return (
    <div>
      <label>
        {labelText}
        <input
          type="checkbox"
          checked={checked}
          onChange={onChange}
        />
      </label>
    </div>
  );
}

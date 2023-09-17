 
  export const notes_validation = {
    name: 'note',
    label: 'Notes',
    multiline: true,
    id: 'note',
    placeholder: 'write notes ...',
    validation: {
      maxLength: {
        value: 100,
        message: '100 characters max',
      },
    },
  }
  
  export const durationMinutes_validation = {
    name: 'durationMinutes',
    label: 'Durantion (Minutes)',
    type: 'number',
    id: 'durationMinutes',
    placeholder: 'write a random number',
    validation: {
      required: {
        value: true,
        message: 'required',
      },
      min: {
        value: 29,
        message: 'Must be greater than 30'
      }
    },
  }
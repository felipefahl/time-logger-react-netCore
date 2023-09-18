 
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
    min: 30,
    id: 'durationMinutes',
    placeholder: 'write a random number',
    validation: {
      required: {
        value: true,
        message: 'required',
      },
      min: {
        value: 30,
        message: 'Must be greater than or equal to 30'
      }
    },
  }

  export const projectName_validation = {
    name: 'name',
    label: 'Name',
    id: 'name',
    placeholder: 'Name ...',
    validation: {
      required: {
        value: true,
        message: 'required',
      },
      maxLength: {
        value: 100,
        message: '100 characters max',
      },
    },
  }

  export const projectDeadline_validation = {
    name: 'deadline',
    label: 'Deadline',
    type: 'date',
    id: 'deadline',
    min: `${(new Date()).toISOString().split('T')[0]}`,
    validation: {
      required: {
        value: true,
        message: 'required',
      },
    },
  }
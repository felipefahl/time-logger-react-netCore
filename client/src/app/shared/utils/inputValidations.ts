 
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

  export const projectName_validation = {
    name: 'name',
    label: 'Name',
    type: 'number',
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
    placeholder: 'YYYY-MM-DD',
    validation: {
      positive: {
        value: v =>  new Date(v) >= new Date(),
        message: 'Must be greater then or equal to Today',
      },
      valueAsDate: {
        value: true,
        message: 'not valid',
      },
      required: {
        value: true,
        message: 'required',
      },
      pattern: {
        value: /^\d{4}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])$/,
        message: 'not valid',
      },
    },
  }
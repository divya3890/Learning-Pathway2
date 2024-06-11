import React, { useState } from 'react';
import { uploadFile } from '../services/api';

const FileUpload = () => {
  const [file, setFile] = useState(null);
  const [message, setMessage] = useState('');
  const [errors, setErrors] = useState([]);

  const handleFileChange = (e) => {
    const selectedFile = e.target.files[0];
    const maxFileSize = 1000000; // 1MB

    if (selectedFile) {
      if (selectedFile.size > maxFileSize) {
        setMessage('File size exceeds the 1MB limit.');
        setFile(null);
        return;
      }

      if (selectedFile.type !== 'text/csv' || !selectedFile.name.endsWith('.csv')) {
        setMessage('Please select a valid CSV file.');
        setFile(null);
        return;
      }

      setFile(selectedFile);
      setMessage('');
    }
  };

  const handleFileUpload = async () => {
    if (file) {
      setMessage('Uploading file...');
      try {
        const response = await uploadFile(file);
        if (response.isValid) {
          setMessage('File uploaded successfully!');
          setErrors([]);
        } else {
          setMessage('File upload completed with errors.');
          setErrors(response.errors);
        }
      } catch (error) {
        setMessage("Error uploading file. Please try again later.");
        setErrors([]);
      }
    } else {
      setMessage("Please select a file to upload.");
      setErrors([]);
    }
  };

  return (
    <>
      <div className="dropzone">
        <input type="file" accept=".csv" onChange={handleFileChange} />
      </div>
      <div>
        <button onClick={handleFileUpload}>Upload</button>
        {message && <p>{message}</p>}
        {errors.length > 0 && (
           <div className='error-container'>
          
           <div className="error-grid">
             <div className="column-header">Error</div>
             <div className="column-header">Value</div>
             {errors.map((error, index) => {
                const [field, message] = error.split('-', 2); // Split the error message using ':' and limit to 2 substrings
                return (
                  <React.Fragment key={index}>
                    <div className="error-item">{field}</div>
                    <div className="error-item">{message}</div>
                  </React.Fragment>
                );
              })}
           </div>
         </div>
        )}
      </div>
    </>
  );
};

export default FileUpload;
import axios from 'axios';


const API_URL = 'https://localhost:7255/api/fileupload';

export const uploadFile = async (file) => {
  const formData = new FormData();
   
  formData.append('FileContent', file);
  formData.append('FileName', file.name);
  formData.append('FileType', file.type);

  try {
    const response = await axios.post(`${API_URL}/MeterReadingUploads`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });
    return response.data;
  } catch (error) {
    console.error("Error uploading file", error);
    throw error;
  }
};




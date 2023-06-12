import axiosInstance from '../common/export'


export default {
  // G
  GetInteractionInfo(Info) {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Interaction/GetInteractionInfo`,{
        params:Info
      })
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },

}

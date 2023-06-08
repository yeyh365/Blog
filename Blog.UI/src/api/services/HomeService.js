import axiosInstance from '../common/export'


export default {
  // G
  GetRecommendInfo() {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Home/GetRecommendInfo`)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },

}

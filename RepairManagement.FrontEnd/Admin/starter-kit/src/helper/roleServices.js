const userInfo = JSON.parse(localStorage.getItem('userInfo'));

export const hasRoles = array => {
  return array.findIndex(x => x == userInfo.role) !== -1
}

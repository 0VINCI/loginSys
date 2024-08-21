import Cookies from 'js-cookie';

export function isAuthenticated() {
  const token = Cookies.get('jwt-auth');
  console.log(!!token)
  return !!token;
}
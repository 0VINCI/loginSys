import Cookies from 'js-cookie';

export function isAuthenticated() {
  const token = Cookies.get('AuthToken');
  console.log(!!token, 'xdd')
  return !!token;
}
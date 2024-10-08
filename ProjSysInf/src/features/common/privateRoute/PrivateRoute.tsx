import { ReactNode } from 'react';
import { Navigate } from 'react-router-dom';
import { isAuthenticated } from './isAuth';

interface PrivateRouteProps {
  children: ReactNode;
}

const PrivateRoute = ({ children }: PrivateRouteProps): JSX.Element => {
  return isAuthenticated() ? <>{children}</> : <Navigate to="/" />;
};

export default PrivateRoute;

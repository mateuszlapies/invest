import {AuthContext} from "../contexts/AuthContext";

export default function Login() {
  return (
    <AuthContext.Consumer>
      {auth => auth.login()}
    </AuthContext.Consumer>
  )
}
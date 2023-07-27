import {createContext, useEffect, useState} from "react";
import passport from 'passport';
import passportSteam from 'passport-steam';
const SteamStrategy = passportSteam.Strategy;

const defaultValue = {
  user: {},
  authenticated: false,
  login: () => undefined,
  logout: () => undefined
}

export const AuthContext = createContext(defaultValue);

const AuthContextProvider = (props) => {
  const [client, setClient] = useState(undefined);
  const [user, setUser] = useState({});
  const [isAuthenticated, setAuthenticated] = useState(false);

  useEffect(() => {
    passport.serializeUser((user, done) => {
      done(null, user);
    });
    passport.deserializeUser((user, done) => {
      done(null, user);
    });

    let url = process.env.DOMAIN;
    if (process.env.PORT) {
      url += ":" + process.env.PORT;
    }

    passport.use('steam', new SteamStrategy({
        returnURL: 'https://' + url + '/login/response',
        realm: 'https://' + url + '/',
        apiKey: process.env.STEAM
      }, function (identifier, profile, done) {
        debugger
        process.nextTick(function () {
          profile.identifier = identifier;
          return done(null, profile);
        });
      }
    ));
    passport.initialize()
    passport.session()
  }, [])

  const login = () => {
    return passport.authenticate('steam', {failureRedirect: '/', redirectIfFound: true}, (req, res) => {
      res.redirect('/')
    })
  }

  const logout = () => {
    console.log(client.endSessionUrl())
  }

  return (
    <AuthContext.Provider value={{
      user: user,
      authenticated: isAuthenticated,
      login: login,
      logout: logout
    }}>
      {props.children}
    </AuthContext.Provider>
  );
};

export default AuthContextProvider;
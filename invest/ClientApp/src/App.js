import {Button, ButtonGroup, ButtonToolbar, Col, Input, InputGroup, InputGroupText, Row} from "reactstrap";
import {useEffect, useState} from "react";
import Item from "./components/Item";
import Editor from "./components/Editor";
import Chart from "./components/Chart";
import {Transition} from "react-transition-group";
import Animation from "./components/Animation";
import AuthContextProvider, {AuthContext} from "./contexts/AuthContext";
import AppRouter from "./AppRouter";

export default function App() {
  return (
    <AppRouter/>
  )
}
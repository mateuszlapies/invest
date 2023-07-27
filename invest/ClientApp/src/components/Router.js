import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";

import Home from "../Home";
import Error from "../Error";
import Login from "./Login";
import Root from "./Root";

export default function Router() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <Root />,
      errorElement: <Error />,
      children: [
        {
          index: true,
          element: <Home />
        },
        {
          path: "login",
          element: <Login />,
        },
        {
          path: "login/response",
          element: <Login />,
        }
      ]
    },
  ]);

  return (
    <RouterProvider router={router} />
  )
}
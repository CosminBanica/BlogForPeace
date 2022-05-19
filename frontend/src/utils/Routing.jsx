import React, { useEffect } from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Blogpost from "../pages/User/Blogpost";
import UserBlogposts from "../pages/User/Blogposts";
//import Blogposts from "../pages/Admin/Blogposts";
import Account from "../pages/User/Account";
import { useAuth0 } from "@auth0/auth0-react";

const Router = () => {
  const { isAuthenticated, loginWithRedirect } = useAuth0();

  useEffect(() => {
    if (!isAuthenticated) {
      loginWithRedirect();
    }
  }, [isAuthenticated, loginWithRedirect]);

  return (
    isAuthenticated && (
      <BrowserRouter>
        <Routes>
            <Route exact path="/" element={<UserBlogposts />} />
            <Route exact path="/profile" element={<Account />} />
            {/*<Route exact path="blogposts" element={<Blogposts />} />*/}
            {/*<Route exact path="blogposts/:id" element={<Blogpost />} />*/}
        </Routes>
      </BrowserRouter>
    )
  );
};

export default Router;
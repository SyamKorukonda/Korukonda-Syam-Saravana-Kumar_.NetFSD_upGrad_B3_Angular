import { inject } from "@angular/core";
import { CanActivateFn,Router } from "@angular/router";

export const authGuard:CanActivateFn=(route,state)=>{
    return isLoggedIn(state.url);
};

function isLoggedIn(requestedUrl:string):boolean{
    alert(requestedUrl);

    const router=inject(Router);

    if(sessionStorage.getItem("AUTH_TOKEN"))
    {
        //alert("Yes...! You are Authenticated");
        return true;
    }
    else{
         //alert("You are not Authenticated");
        // redirect to login 

        // router.navigate(['/login']);

        router.navigate(['/login'],{
            queryParams:{returnUrl:requestedUrl}
        });

        return false;
    }
}
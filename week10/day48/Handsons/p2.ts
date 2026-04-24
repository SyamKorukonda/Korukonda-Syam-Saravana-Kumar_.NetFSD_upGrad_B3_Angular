function getWelcomeMeg(name:string):string
{
    return `Welcome ${name} have a nice day.`;
}

function getUserInfo(name: string, age?: number): string {
  if (age !== undefined) {
    return `User ${name} is ${age} years old.`;
  }
  return `User ${name} age is not provided.`;
}

function gerSubscriptionStatus(name:string, isSubscribed:boolean=false):string
{
    return isSubscribed ?`${name} is subscribed to the service.`
                        : `${name} is not subscribed.`;
}

function isEligibleForPremium(age: number): boolean {
  return age > 18;
}

//arrow function
const getAccountUpdate = (name: string): string => {
  return `Account details updated successfully for ${name}.`;
};

const notificationService = {
  appName: "MyApp",

  sendNotification: (message: string): string => {
    return `[${notificationService.appName}] ${message}`;
  }
};

console.log(getWelcomeMeg("Syam"));

console.log(getUserInfo("Syam", 21));
console.log(getUserInfo("Ram"));

console.log(gerSubscriptionStatus("Syam", true));
console.log(gerSubscriptionStatus("Ram"));

console.log("Eligible for Premium:", isEligibleForPremium(21));

console.log(getAccountUpdate("Syam"));

console.log(notificationService.sendNotification("Welcome to the system!"));
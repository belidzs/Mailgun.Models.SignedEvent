# Mailgun.Models.SignedEvent for incoming Mailgun webhooks

[![nuget shield](https://img.shields.io/nuget/v/Mailgun.Models.SignedEvent)](https://www.nuget.org/packages/Mailgun.Models.SignedEvent)

Implements data model for incoming Mailgun events to use with your custom Mailgun webhooks.

This library can be used as a data model for the deserialization of this data with any JSON serializer of your choice. It even provides a handy function to [verify its cryptographic signature](https://documentation.mailgun.com/en/latest/user_manual.html#webhooks).

Since it targets .NET Standard 1.6 it is [compatible](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) with a wide variety of platforms (such as .NET Framework 4.6.1, .NET Core 1.0 and newer).

## Background

According to Mailgun they provide *developer friendly* transactional e-mail service. In spite of this claim there's still no official SDK for nearly any platform and their documentation often lacks basic information. However it's still one of the best options you have if you don't want to implement your own e-mail delivery service which can become very complex very quickly.

Once you have an account with them you can [subscribe](https://documentation.mailgun.com/en/latest/user_manual.html#webhooks) to various messaging events so when the appropriate event happens (eg. the e-mail was delivered or bounced) Mailgun will POST a JSON encoded object to the URL you provided.

This library was created to ease the burden of deserializing these events into POCOs and to provide an easy way to verify the cryptographic signature of an incoming packet.

## Usage

### Installation

Add this package as a dependency to your project using [NuGet](https://www.nuget.org/packages/Mailgun.Models.SignedEvent).

### Example

Here's a practical example using ASP.NET Core 3.1:

```csharp
using Mailgun.Models.SignedEvent;
```

...

```csharp
[Route("[controller]")]
[ApiController]
public class DeliveredController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<SignedEvent>> PostDelivered([FromBody] SignedEvent signedEvent)
    {
        // ASP.NET Core framework will automagically deserialize JSON into signedEvent (see notes at the bottom regarding caveats)
        if (!signedEvent.Signature.IsValid("your-api-key"))
        {
            // if the signature is invalid, return 401
            return Unauthorized();
        }

        // do something meaningful with signedEvent.Event here

        // finally return 201 so Mailgun knows POST has been successful. Otherwise it'll keep retrying
        return CreatedAtAction(nameof(PostDelivered), null);
    }
}
```

A `SignedEvent` contains a `Signature` and the actual `Event`. While not mandatory it's recommended to check the signature to make sure it was actually signed by Mailgun.

Since the signature is created using the signing server's own time you can specify how old a signature can be to still be considered as valid. By default this is set to 10 minutes.

## Important Notes
### Unusual JSON Naming Convention

Mailgun generates JSON data using an unusual naming convention with dashes between words. This means that the verb `is valid` will be encoded as `is-valid` even though the convention is to encode names as *camelCase* (resulting in `isValid`).

[There are ways](https://github.com/belidzs/DashedJsonNamingPolicy) to configure most JSON serializers to handle this, but this topic is out of scope of this project.

### Structure Is Constantly Changing
Please note that these events [can apparently change their structure anytime](https://documentation.mailgun.com/en/latest/api-events.html#event-structure), so don't be surprised if the data you've just received contains new fields.

If you discover a change (which can only be an addition according to their documentation) you're welcome to open a PR.

# ProfileService

<h2>General</h2>

The service provides data about profiles. He also has methods that allow you to: create a profile, delete a profile, update a profile and perform verification of the Wax wallet

<h2>gRPC Methods</h2>

| Method | Description |
| ------ | ------ |
| GetByDiscordId(GetByDiscordIdRequest) : GetByDiscordIdResponse | Get profile by discrod id |
| GetRangeOfProfiles(GetRangeOfProfilesRequest) : GetRangeOfProfilesResponse | Get a list of profiles |
| CreateProfile(CreateProfileRequest) : CreateProfileResponse | Create new profile |
| DeleteUser(DeleteUserRequest) : DeleteUserResponse | Delete profile |
| DepositPoints(DepositPointsRequest) : DepositPointsResponse | Adds point to the profile |
| WithdrawPoints(WithdrawPointsRequest) : WithdrawPointsResponse | Remove point to the profile |
| VerifyWaxWallet(VerifyWaxWalletRequest) : VerifyWaxWalletResponse | Update construction level |

<h2>Technologies</h2>

Database: PostgreSQL

Database contains one collections:

- Profile

<h2>Entity examples</h2>

Profile entity:

```
"profile": 
{
	"id": "88861980-1e4c-4a01-b06f-e6eebb47bbf1",
	"discord_id": 000000000000000000,
	"wax_wallet": "user.wallet",
	"points": 0,
	"win_count": 0,
	"lose_count": 0,
	"creation_date": "10.10.2022 19:00:00"
}
```

<h2>Request examples</h2>

GetByDiscordIdRequest:

```
{
	"discord_id": 000000000000000000
}
```

GetRangeOfProfilesRequest:

```
{
	"page": 1,
	"page_size": 10
}
```

CreateProfileRequest:

```
{
	"discord_id": 000000000000000000
}
```

DeleteUserRequest:

```
{
	"discord_id": 000000000000000000
}
```

DepositPointsRequest:

```
{
	"discord_id": 000000000000000000,
	"points": 0
}
```

WithdrawPointsRequest:

```
{
	"discord_id": 000000000000000000,
	"points": 0
}
```

VerifyWaxWalletRequest:

```
{
	"profile": 
	{
		"id": "88861980-1e4c-4a01-b06f-e6eebb47bbf1",
		"discord_id": 000000000000000000,
		"wax_wallet": "user.wallet",
		"points": 0,
		"win_count": 0,
		"lose_count": 0,
		"creation_date": "10.10.2022 19:00:00"
	}
}
```

<h2>Response examples</h2>

GetByDiscordIdResponse:

```
{
	"profile": 
	{
		"id": "88861980-1e4c-4a01-b06f-e6eebb47bbf1",
		"discord_id": 000000000000000000,
		"wax_wallet": "user.wallet",
		"points": 0,
		"win_count": 0,
		"lose_count": 0,
		"creation_date": "10.10.2022 19:00:00"
	}
}
```

GetRangeOfProfilesResponse:

```
{
	"profiles": [
		{
			"id": "88861980-1e4c-4a01-b06f-e6eebb47bbf1",
			"discord_id": 000000000000000000,
			"wax_wallet": "user.wallet",
			"points": 0,
			"win_count": 0,
			"lose_count": 0,
			"creation_date": "10.10.2022 19:00:00"
		},
		{
			"id": "74920362-5y5p-0v12-j20u-x5iigg76kff8",
			"discord_id": 000000000000000000,
			"wax_wallet": "user.wallet",
			"points": 0,
			"win_count": 0,
			"lose_count": 0,
			"creation_date": "11.11.2022 20:00:00"
		},
	]
}
```

CreateProfileResponse:

```
{
	"status": "STATUS_TYPE_SUCCESS"
}
```

DeleteUserResponse:

```
{
	"status": "STATUS_TYPE_SUCCESS"
}
```

DepositPointsResponse:

```
{
	"status": "STATUS_TYPE_SUCCESS"
}
```

WithdrawPointsResponse:

```
{
	"status": "STATUS_TYPE_SUCCESS"
}
```

VerifyWaxWalletResponse:

```
{
	"status": "STATUS_TYPE_SUCCESS"
}
```

<h2>Status type enum</h2>

```
enum StatusType {
	STATUS_TYPE_UNSPECIFIED = 0;
	STATUS_TYPE_SUCCESS = 1;
	STATUS_TYPE_FAILED = 2;
}
```
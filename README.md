
# README

Instructions for running the Gomoku solution:

1. Run the Gomoku solution. (I’m using Visual Studio 2022 Community version).
2. Swagger will be shown to display available API endpoints you can execute. (You can also use Postman if preferred).







## API Reference

#### Place a stone
Allows a player to place a stone on the board for his/her turn

```http
  POST /Gomoku/PlaceAStone
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `player` | `string` | **Required**. Can either be "Player1" or "Player2" only. This is to ensure only two players are playing. |
| `stoneRow` | `int` | **Required**. x coordinate of the target placement of the stone. Can be between 0 - 14 (0 based indexing). |
| `stoneColumn` | `int` | **Required**. y coordinate of the target placement of the stone. Can be between 0 - 14 (0 based indexing). |

#### Display board
Displays the current board status for visualization

```http
  GET /Gomoku/DrawBoard
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to fetch |

3. You can also run unit tests for automated basic scenario testing.


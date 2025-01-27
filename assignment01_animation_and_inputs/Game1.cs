using System.Text.Json.Nodes;
using day03_animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace assignment01_animation_and_inputs;

public class Game1 : Game
{
    private const int _WindowWidth = 1024;
    private const int _WindowHeight = 709;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _background, _wolf;
    private CelAnimationSequence _jarAnimation;
    private CelAnimationPlayer _jarPlayer;
    private CelAnimationSequenceMultiRow _flyIdleAnimation, _flyRightAnimation, _flyLeftAnimation;
    private CelAnimationPlayerMultiRow _idlePlayer, _rightPlayer, _leftPlayer;
    private int _batx = 512;
    private int _baty = 200;
    private int _jary = 0;
    private KeyboardState kbCurrentState;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        //background
        _background = Content.Load<Texture2D>("cathedral");
        //other image
        _wolf = Content.Load<Texture2D>("howl");
        //single row sequence
        Texture2D jarSheet = Content.Load<Texture2D>("jar");
        _jarAnimation = new CelAnimationSequence(jarSheet, 32, 1f);
        _jarPlayer = new CelAnimationPlayer();
        _jarPlayer.Play(_jarAnimation);
        //multi row sequence
        Texture2D batSheet = Content.Load<Texture2D>("bat");
        _flyIdleAnimation = new CelAnimationSequenceMultiRow(batSheet, 48, 64, 0.2f, 3);
        _flyRightAnimation = new CelAnimationSequenceMultiRow(batSheet, 48, 64, 0.2f, 2);
        _flyLeftAnimation = new CelAnimationSequenceMultiRow(batSheet, 48, 64, 0.2f, 4);
        _idlePlayer = new CelAnimationPlayerMultiRow();
        _idlePlayer.Play(_flyIdleAnimation);
        _rightPlayer = new CelAnimationPlayerMultiRow();
        _rightPlayer.Play(_flyRightAnimation);
        _leftPlayer = new CelAnimationPlayerMultiRow();
        _leftPlayer.Play(_flyLeftAnimation);
    }

    protected override void Update(GameTime gameTime)
    {
        kbCurrentState = Keyboard.GetState();
        if (kbCurrentState.IsKeyDown(Keys.Right))//"Keys.Down" represents the down arrow on the keyboard
        {
            _batx++;
            _rightPlayer.Update(gameTime);
        }
        else if (kbCurrentState.IsKeyDown(Keys.Left))
        {
            _batx--;
            _leftPlayer.Update(gameTime);
        }
        else
        {
            _idlePlayer.Update(gameTime);
        }
        if (kbCurrentState.IsKeyDown(Keys.Down)){
            _jary++;
        }

        _jarPlayer.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_wolf, new Vector2(0, 0), Color.White);
        if (kbCurrentState.IsKeyDown(Keys.Right))//"Keys.Down" represents the down arrow on the keyboard
        {
            _rightPlayer.Draw(_spriteBatch, new Vector2(_batx, _baty), SpriteEffects.None);
        }
        else if (kbCurrentState.IsKeyDown(Keys.Left))
        {
            _leftPlayer.Draw(_spriteBatch, new Vector2(_batx, _baty), SpriteEffects.None);
        }
        else
        {
            _idlePlayer.Draw(_spriteBatch, new Vector2(_batx, _baty), SpriteEffects.None);
        }
        
        _jarPlayer.Draw(_spriteBatch, new Vector2(100, _jary), SpriteEffects.None);
        //_idlePlayer.Draw(_spriteBatch, new Vector2(_batx, 200), SpriteEffects.None);
        //_rightPlayer.Draw(_spriteBatch, new Vector2(_batx, 300), SpriteEffects.None);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
